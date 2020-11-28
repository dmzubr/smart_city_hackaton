export class OuterDataSourceModel {
    public constructor(
      public outerMetricDataSourceId: number,
      public outerMetricId: number,
      public name: string,
      public handlerUrl: string
    ) { }
}

export class OuterDataSourceModelViewModel extends OuterDataSourceModel {
  public constructor(
    public outerMetricDataSourceId: number,
    public outerMetricId: number,
    public name: string,
    public handlerUrl: string,
    public metricName: string
  ) {
    super(outerMetricDataSourceId, outerMetricId, name, handlerUrl);
  }
}
