using System.Reflection;
using AutoFixture.Kernel;

namespace AutoFixture.AutoMoq_cookbook;

public class TimeOnlySpecimenBuilder : ISpecimenBuilder
{
    private readonly RandomNumericSequenceGenerator _randomGenerator;

    public TimeOnlySpecimenBuilder()
    {
        var minDate = TimeOnly.MinValue.ToTimeSpan();
        var maxDate = TimeOnly.MaxValue.ToTimeSpan();
        _randomGenerator = new RandomNumericSequenceGenerator((long)minDate.TotalMinutes, (long)maxDate.TotalMinutes);
    }

    public object Create(object request, ISpecimenContext context)
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        return IsTimeOnlyRequest(request) ? CreateRandomTime(context) : new NoSpecimen();
    }

    private static bool IsTimeOnlyRequest(object request) => typeof(TimeOnly).GetTypeInfo().IsAssignableFrom(request as Type);

    private object CreateRandomTime(ISpecimenContext context)
    {
        var minutes = (int)_randomGenerator.Create(typeof(int), context);
        var timeSpan = TimeSpan.FromMinutes(minutes);
        return TimeOnly.FromTimeSpan(timeSpan);
    }
}