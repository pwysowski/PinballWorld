using System;

public interface IBumper
{
    Action<float> OnBump { get; set; }
}