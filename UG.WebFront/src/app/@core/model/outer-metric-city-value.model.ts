export class OuterMetricCityValueModel {
    public constructor(
      public outerMetricCityValueId: number,
      public outerMetricId: number,
      public cityId: number,
      public value: number,
      public calcDate: Date,
      public periodStart: Date,
      public periodEnd: Date
    ) { }
}

export class OuterMetricValueViewModel {
  public constructor(
    public outerMetricCityValueId: number,
    public outerMetricId: number,
    public metricName: string,
    public cityName: string,
    public value: number,
    public year: number
  ) { }
}
