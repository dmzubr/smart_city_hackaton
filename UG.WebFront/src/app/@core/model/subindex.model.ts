import { IndicatorViewModel } from './indicator.model'

export class SubindexModel {
    public constructor(
      public subIndexId: number,
      public name: string
    ) { }
}

export class SubindexViewModel extends SubindexModel{
  public constructor(
    public subIndexId: number,
    public name: string,
    public indicatorsList: IndicatorViewModel[]
  ) {
    super(subIndexId, name);
  }
}
