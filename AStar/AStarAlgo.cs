using System;
using System.Collections.Generic;

using AStar;
using AStar.Source;
using AStar.CoordFind;
using AStar.CoordFind.FunctionSet;
using AStar.CoordFind.SquareMatrix;
using AStar.CoordFind.SquereSearchers;


namespace AStar
{
    /// <summary>
    /// Класс алгоритма поиска кратчайшего пути в графе А*
    /// </summary>
    public class AStarAlgo
    {
        Graph graph;

        SearcherNodes sqr_search_nodes;
        SearcherEdges sqr_search_edges;

        IAstarFunctionSet functionSet;

        /// <summary>
        /// Конструктор поиска кратчайшего пути для графа graph и набором функций functionSet
        /// </summary>
        /// <param name="graph">Граф внутри которого производится поиск </param>
        /// <param name="functionSet"> Набор функций с помощью которых будет производится поиск</param>
        public AStarAlgo(Graph graph, IAstarFunctionSet functionSet)
        {
            if (graph == null) throw new ArgumentNullException(nameof(graph));
            if (functionSet == null) throw new ArgumentNullException(nameof(functionSet));

            this.functionSet = functionSet;

            this.graph = graph;
        }

        /// <summary>
        /// Конструктор поиска кратчайшего пути для графа graph и набором функций для 2D плоскости по-умолчанию
        /// </summary>
        /// <param name="graph">Граф внутри которого будет производится поиск</param>
        public AStarAlgo(Graph graph)
        {
            if (graph == null) throw new ArgumentNullException(nameof(graph));

            this.functionSet =  new Astar2DFunctionSet();
            this.graph = graph;
        }

        /// <summary>
        /// Нахождение кратчайшего пути между двумя вершинами в графе по ИНДЕКСУ вершины
        /// </summary>
        /// <param name="node_start">Индекс стартовой вершины</param>
        /// <param name="node_finish">Индекс конечной вершины</param>
        /// <returns>Возвращает список ребер из которых строится маршут из node_start к node_finish, при невозможности его построения - Null</returns>               
        public Path MakePath_byIndexNode(int node_start, int node_finish)
        {
            NodeInWork node_strart_link;
            NodeInWork node_finish_link;
            if (graph.TryGetValue(node_start, out node_strart_link))
                if (graph.TryGetValue(node_finish, out node_finish_link))
                    return MakePath(node_strart_link, node_finish_link);

            return null;
        }

        /// <summary>
        /// Нахождение кратчайшего пути между двумя вершинами в графе по ССЫЛКАМ на вершины
        /// </summary>
        /// <param name="node_start">Ссылка на стартовую вершину</param>
        /// <param name="node_finish">Ссылка на конечную вершину</param>
        /// <returns>Возвращает список ребер из которых строится маршут из node_start к node_finish, при невозможности его построения - Null</returns>        
        public Path MakePath(NodeInWork node_strart_link, NodeInWork node_finish_link)
        {
            if (node_strart_link == node_finish_link || node_strart_link==null || node_finish_link ==null)
                return null;

            //Инициализация коллекций для алгоритма А*

            //Открытый список
            Dictionary<NodeInWork, OpenListValue> open = new Dictionary<NodeInWork, OpenListValue>();
            //Сортированный список эвритических фукнций открытого списка
            List<OpenListValue> open_sorted = new List<OpenListValue>();
            //Маршрутный список            
            Dictionary<NodeInWork, RoadInfo> road_map = new Dictionary<NodeInWork, RoadInfo>();


            //Стартовая инициализация алгоритма
            //Сохранения ключевых ссылок графа
            var opn_start = new OpenListValue(node_strart_link, 0, 0);

            open.Add(node_strart_link, opn_start);
            open_sorted.Add(opn_start);
            road_map.Add(node_strart_link, new RoadInfo(null, null));
            NodeInWork curr = null;

            //Проход алгоритма А*
            do
            {
                var opn_curr_weight = open_sorted[open_sorted.Count - 1];
                curr = opn_curr_weight.Node;

                //Если финальная нода была признана самой "легкой", то лучше маршрута уже не будет. Конец алгоритма.
                if (curr == node_finish_link)
                    break;

                open.Remove(curr);
                road_map[curr].close = true;

                open_sorted.RemoveAt(open_sorted.Count - 1);

                //Проверка всех соседей пограничной вершины на предмет веса их маршрута + эвристики                                      
                foreach (EdgeInWork edge in curr.edge)
                {
                    RoadInfo rdm;
                    road_map.TryGetValue(edge.Node_in, out rdm);

                    if (rdm == null || !rdm.close) //Проверка на вхожение вершины в закрытый список
                    {
                        OpenListValue olv;
                        bool flag = false;
                        if (rdm == null) //(!open.ContainsKey(edge.node_in)) -- Если записи данной вершины нет, создать её с параметрами по-умолчанию
                        {
                            olv = new OpenListValue(edge.Node_in);
                            olv.RealWeight = opn_curr_weight.RealWeight + edge.weight;
                            olv.HeuristicWeight = olv.RealWeight + functionSet.heuristic_between_point(edge.Node_in, node_finish_link);

                            open.Add(edge.Node_in, olv);
                            road_map.Add(edge.Node_in, new RoadInfo(curr, edge));
                            flag = true;
                        }
                        else
                        {
                            olv = open[edge.Node_in];
                            if (olv.RealWeight > opn_curr_weight.RealWeight + edge.weight) //Если вес записанный в вершине больше чем новополученный
                                                                                                    //Сохраняем новые реальные и эвристические веса
                            {                                                                     //Параметры по-умолчанию безусловно входят в эту ветвь
                                                                                                  //Удаляем старую эвристу из сортированного списка
                                int index = open_sorted.BinarySearch(olv);
                                while (open_sorted[index] != olv)
                                    index++;
                                open_sorted.RemoveAt(index);

                                olv.RealWeight = opn_curr_weight.RealWeight + edge.weight;
                                olv.HeuristicWeight = olv.RealWeight + functionSet.heuristic_between_point(edge.Node_in, node_finish_link);
                                rdm.prev = curr;
                                rdm.edge = edge;
                                flag = true;
                            }
                        }

                        //Добавляем еще одну запись в сортированный список с сохранением сортировки
                        if (flag)
                        {
                            int findex = open_sorted.BinarySearch(olv);
                            if (findex < 0)
                                open_sorted.Insert(~findex, olv);
                            else
                                open_sorted.Insert(findex, olv);
                        }
                    }
                }
            }
            //Если открытый список пуст, то конец алгоритма
            while (!(open.Count == 0));


            //Ecли в маршрутном списке небыла создана запись под финальную точку или она соотве
            if ((road_map.ContainsKey(node_finish_link) == false))
            {
                return null;
            }
            else
            {
                List<EdgeInWork> result = new List<EdgeInWork>();
                do
                {
                    if (road_map[curr].prev == null)
                        break;
                    else
                    {
                        result.Add(road_map[curr].edge);
                        curr = road_map[curr].prev;
                    }
                }
                while (true);

                result.Reverse();

                return new Path(result);
            }
        }

        /// <summary>
        /// Нахождение кратчайшего пути между двумя вершинами в графе по координатам точек (ищутся ближайшие вершины к точкам)
        /// </summary>
        /// <param name="point">Координаты стартовой точки</param>
        /// <param name="xpoint">Координаты конечной точки</param>
        /// <returns></returns>
        public Path MakePath_byNodesCoordinates(IPoint point, IPoint xpoint)
        {
            if (point == null) throw new ArgumentNullException(nameof(point));
            if (xpoint == null) throw new ArgumentNullException(nameof(xpoint)); 

            return MakePath(SqrNodesSearch.Find(point) as NodeInWork, SqrNodesSearch.Find(xpoint) as NodeInWork);

        }

        /// <summary>
        /// Нахождение кратчайшего пути между двумя вершинами в графе по КООРДИНАТАМ в пространстве
        /// </summary>
        /// <param name="point">Координаты стартовой точки</param>
        /// <param name="xpoint">Координаты конечной точки</param>
        /// <returns>Возвращает список включающий себя список ребер из которых строится маршут из node_start к node_finish, при невозможности его построения - Null</returns>
        public ResultRoute MakeRouteResult_byNodesCoordinates(IPoint point, IPoint xpoint)
        {
            if (point == null) throw new ArgumentNullException(nameof(point));
            if (xpoint == null) throw new ArgumentNullException(nameof(xpoint));

            var start_mitm = SqrEdgesSearch.Find(point);

            var finish_mitm = SqrEdgesSearch.Find(xpoint);


            Path route = null;
            if (!start_mitm.Point_To.Equals(finish_mitm.Point_To))
            {
                route = MakePath_byNodesCoordinates(start_mitm.StartPath, finish_mitm.FinishPath);
                return new ResultRoute(route, start_mitm, finish_mitm);
            }
            else            
                return new ResultRoute (null, start_mitm, finish_mitm);
        }

        /// <summary>
        /// Поиск ближайшей к Point вершины в графе
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public IPoint FindNearNode (IPoint point)
        {
            return SqrNodesSearch.Find(point);
        }

        /// <summary>
        /// Поиск ближайшего к Point ребра в графе
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public DropOnEdge FindNearEdge(IPoint point)
        {
            return SqrEdgesSearch.Find(point);
        }



        /// <summary>
        /// Квадратный поиск вершин для существующего графа
        /// </summary>
        internal SearcherNodes SqrNodesSearch
        {
            get
            {
                if (sqr_search_nodes == null)                     
                        sqr_search_nodes = new SearcherNodes(graph.Nodes, functionSet);                
                return sqr_search_nodes;
            }
        }

        /// <summary>
        /// Квадратный поиск ребер для существующего графа
        /// </summary>
        internal SearcherEdges SqrEdgesSearch
        {
            get
            {
                if (sqr_search_edges == null)
                    sqr_search_edges = new SearcherEdges(graph.Nodes, functionSet);
                return sqr_search_edges;
            }
        }
    }
}