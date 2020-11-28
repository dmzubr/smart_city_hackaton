import { IndicatorIndexValueModel } from './indicator-index-value.model'

export class IndicatorValueModel {
    public constructor(
      public indicatorId: number,
      public cityId: number,
      public value: number,
      public year: number
    ) { }
}

export class ValuesContainerModel {
  public constructor(
    public indicatorValuesList: IndicatorValueModel[],
    public indicatorIndexesValuesList: IndicatorIndexValueModel[]
  ) { }
}
