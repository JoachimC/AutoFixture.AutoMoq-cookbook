using System.Reflection;
using AutoFixture.Kernel;

namespace AutoFixture.AutoMoq_cookbook;

public class DateOnlySpecimenBuilder : ISpecimenBuilder
{
    private readonly RandomNumericSequenceGenerator _randomGenerator;

    public DateOnlySpecimenBuilder()
    {
        var minDate = DateOnly.FromDateTime(DateTime.Today.AddYears(-10));
        var maxDate = DateOnly.FromDateTime(DateTime.Today.AddYears(10));
        _randomGenerator = new RandomNumericSequenceGenerator(minDate.DayNumber, maxDate.DayNumber);
    }

    public object Create(object request, ISpecimenContext context)
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        return IsDateOnlyRequest(request) ? CreateRandomDate(context) : new NoSpecimen();
    }

    private static bool IsDateOnlyRequest(object request) => typeof(DateOnly).GetTypeInfo().IsAssignableFrom(request as Type);

    private object CreateRandomDate(ISpecimenContext context) => DateOnly.FromDayNumber((int)_randomGenerator.Create(typeof(int), context));
}