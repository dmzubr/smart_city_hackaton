import { Component, OnInit } from '@angular/core';
import {ActivatedRoute, Router} from '@angular/router';

import { IndicatorSocialVerificationService, IndicatorService } from '../../@core/services'
import {
  SocialVerificationRecordModel, IndicatorModel
} from "../../@core/model";
import { NbGlobalPhysicalPosition, NbToastrService } from "@nebular/theme";
import { UserContextHelper } from '../../@core/utils';

@Component({
  selector: 'verification-details',
  templateUrl: './verification-details.component.html',
  styleUrls: ['./verification-details.component.scss'],
  providers:[
    IndicatorSocialVerificationService, IndicatorService
  ]
})
export class VerificationDetailsComponent implements OnInit {

  public allRecords: SocialVerificationRecordModel[] = [];
  public records: SocialVerificationRecordModel[] = [];
  public selectedRecordsTypesList: string[]  = [
    'Все записи',
    'Только негативные'
  ];
  public selectedRecordsType: string = this.selectedRecordsTypesList[0];

  sub: any;
  private indicatorId: number = 0;
  public indicator: IndicatorModel = this.indicatorService.GetEmptyObj();

  constructor(
    private route: ActivatedRoute,
    public userHelper: UserContextHelper,
    private dataService: IndicatorSocialVerificationService,
    private indicatorService: IndicatorService,
    private toastrService: NbToastrService,
    private router: Router){ }

  ngOnInit() {
    this.sub = this.route.params.subscribe(params => {
      this.indicatorId = +params['indicator-id'];
      if (this.indicatorId > 0) {
        this._initRecordsList();
        this.indicatorService.Get(this.indicatorId).subscribe(res => {
          this.indicator = res;
        });
      }
    });
  }

  private showToast(msg, status) {
    this.toastrService.show(
      '',
      msg,
      {  position: NbGlobalPhysicalPosition.TOP_RIGHT,  status: status }
    );
  }

  private _initRecordsList(){
    this.dataService.GetSocialRecordsListByIndicator(this.indicatorId).subscribe(res => {
      this.allRecords = res;
      this.records = res;
    });
  }

  public changeRecordTypes() {
    if (this.selectedRecordsType == this.selectedRecordsTypesList[1]) {
      // Filter records only with texts that were marked as negative
      this.records = this.allRecords.filter(X => X.emotionMark == 0);
    } else {
      this.records = this.allRecords;
    }
  }

  public markRecordAsNotAggressive(record: SocialVerificationRecordModel) {
    this.showToast('Запись помечена как не имеющая признаков негативного выражения.', 'danger');
    this.records = this.allRecords.filter(x => {
      x.sNCommentId != record.sNCommentId && x.sNWallPostId != record.sNWallPostId
    });
  }
}
