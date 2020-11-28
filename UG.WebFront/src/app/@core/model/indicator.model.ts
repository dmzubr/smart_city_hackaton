import { IndicatorIndexModel } from './indicator-index.model'

export class IndicatorModel {
    public constructor(
      public indicatorId: number,
      public subIndexId: number,
      public name: string,
      public number: number
    ) { }
}

export class IndicatorViewModel extends IndicatorModel {
  public constructor(
    public indicatorId: number,
    public subIndexId: number,
    public name: string,
    public number: number,
    public indicatorIndexes: IndicatorIndexModel[]
  ) {
    super(indicatorId, subIndexId, name, number);
  }
}
