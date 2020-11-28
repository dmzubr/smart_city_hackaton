export class IndicatorSocialVerificationModel {
    public constructor(
      public indicatorSocialVerificationId: number,
      public indicatorId: number,
      public cityId: number,
      public value: number,
      public calcDate: Date,
      public periodStart: Date,
      public periodEnd: Date,
      public ratingPosition: number,
      public ratingClass: string
    ) { }
}
