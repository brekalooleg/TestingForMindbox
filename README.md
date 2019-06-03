# TestingForMindbox
Брекало Олег, соискатель на вакансию
Вопрос 1: Напишите библиотеку для поставки внешним клиентам, которая умеет вычислять площадь круга по радиусу и треугольника по трем сторонам. Дополнительно к работоспособности оценим:

·         Юнит-тесты

·         Легкость добавления других фигур

·         Вычисление площади фигуры без знания типа фигуры

·         Проверку на то, является ли треугольник прямоугольным

Реализовал библиотеку AreaDLL. В ней реализован интерфейс внутри которого подсчет площади и 2 класса его реализующие - круг и треугольник + юниттестирование существующего функционала. Новые фигуры могут реализовывать этот интерфейс, а так же вызов функции у интефейса позволит высчитывать площадь не зная точного типа. У треугольника реализована функция IsRightTriangle для проверки его прямоугольности.

Дополнение к Вопросу 1: Прикладываю отдельно библиотеку реализующую алгоритм A*, которая была реализованна мной в рамках одного из картографических проектов внутри института. Благо в ней я точно уверен что засекретить А-стар точно ни у кого не получилось.

Вопрос 2: SQL Запрос описан в "Task 2.txt" внутри репозитория.
