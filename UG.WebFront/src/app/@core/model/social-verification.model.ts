export class SocialVerificationRecordModel {
    public constructor(
      public sNWallPostId: number,
      public sNCommentId: number,
      public text: string,
      public publishDateTime: number,
      public url: string,
      public emotionMark: number,

      public likesQuantity?: number,
      public repostQuantity?: number,
      public commentQuantity?: number,
      public viewsQuantity?: number
) { }
}
