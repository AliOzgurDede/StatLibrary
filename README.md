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
Pattern (Stationary, Trending, Seasonal): enum |
MinimumValue: double     |
MaximumValue: double     |
Range: double            |
  
Parameters    |
------------- |
Mean(DataSet<double>): double|
StandartDeviation(DataSet<double>): double|
Skewness(DataSet<double>): double|
Covariance(DataSet<double>): double|
CorrelationCoefficient(DataSet<double>): double|
Zvalue(DataSet<double>, double, double): double|
Ztest((DataSet<double>, double, double, double, double, bool): bool|

Generators<T>|
-------------|
GeneratingFromDataGridView(DataSet<T>, DataGridView): void +3 overloads|
GeneratingFromListBox(DataSet<T>, ListBox): void +3 overloads|

## License
[MIT](https://choosealicense.com/licenses/mit/)
