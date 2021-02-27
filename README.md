# StatLibrary
Ready-to-use Class Library for Statistical Computations

## Usage
To use this library, it is necessary to create an instance of DataSet collection 
```csharp
DataSet<T> "YourDataSetName" = new DataSet<T>();
```
DataSet collection class is inherited from C# List collection class
```csharp
public class DataSet<T> : List<T>
```

## Class Structure

DataSet<T>               |
-------------            |
Pattern [Stationary, Trending, Seasonal]: enum |
MinimumValue: double     |
MaximumValue: double     |
Range: double            |
Slope: double            |
Intercept: double        |
NumberOfSeasons: int     |
SeasonSize: int          |
SeasonalFactors[ ]: double  |
MovingAverages(int Index, int Step): double  |
ExponentialSmoothing(int Index, double Alpha): double  |
LinearRegression(double X): double  |
Estimate(int Index): double  |

Parameters    |
------------- |
Mean(DataSet<double>): static double|
StandartDeviation(DataSet(double)): static double|
Skewness(DataSet(double)): static double|
Covariance(DataSet(double)): static double|
CorrelationCoefficient(DataSet(double)): static double|
Zvalue(DataSet(double), double, double): static double|
Ztest((DataSet(double), double, double, double, double, bool): static bool|

Generators<T>|
-------------|
GeneratingFromDataGridView(DataSet(T), DataGridView): static void +3 overloads|
GeneratingFromListBox(DataSet(T), ListBox): static void +3 overloads|

## License
[MIT](https://choosealicense.com/licenses/mit/)
