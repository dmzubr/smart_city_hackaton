import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

import { SubindexService, IndicatorValueService, CityService } from '../../@core/services'
import {
  SubindexViewModel, ValuesContainerModel, CityModel,
  IndicatorIndexValueModel, IndicatorSocialVerificationModel, IndicatorViewModel
} from "../../@core/model";
import { NbGlobalPhysicalPosition, NbToastrService } from "@nebular/theme";
import {UserContextHelper} from "../../@core/utils";
import {IndicatorIndexValueService} from "../../@core/services/indicator-index-value.service";
import {IndicatorSocialVerificationService} from "../../@core/services/indicator-social-verification.service";

@Component({
  selector: 'indicators-list',
  templateUrl: './indicators-list.component.html',
  styleUrls: ['./indicators-list.component.scss'],
  providers:[
    SubindexService, IndicatorValueService, CityService, IndicatorIndexValueService,
    IndicatorSocialVerificationService
  ]
})
export class IndicatorsListComponent implements OnInit {

  public subIndexesAllList: SubindexViewModel[] = [];
  public subIndexesList: SubindexViewModel[] = [];
  private valuesContainer: ValuesContainerModel = this.indicatorValueService.GetEmptyContainer();
  public socialVerificationsList: IndicatorSocialVerificationModel[] = [];

  public citiesList: CityModel[] = [];
  public selectedCityId: number = 0;

  public yearsList: string[] = [
    '2020',
    '2019',
    '2018'
  ];
  public selectedYear = '2019';

  public filteredSubindexes = [];

  constructor(
    public userHelper: UserContextHelper,
    private subindexService: SubindexService,
    private indicatorValueService: IndicatorValueService,
    private indicatorIndexValueService: IndicatorIndexValueService,
    private cityService: CityService,
    private socialVerificationService: IndicatorSocialVerificationService,
    private toastrService: NbToastrService,
    private router: Router){ }

  ngOnInit() {
    this._initLookups();
    this._initSubindexesList();
    this._initValuesList();
    this._initSocialVerificationMarks();
  }

  private showToast(msg, status) {
    this.toastrService.show(
      '',
      msg,
      {  position: NbGlobalPhysicalPosition.TOP_RIGHT,  status: status }
    );
  }

  private _initSubindexesList(){
    this.subindexService.GetVMList().subscribe(res => {
      this.subIndexesList = res;
      this.subIndexesAllList = res;
    });
  }

  private _initValuesList(){
    this.indicatorValueService.GetValuesContainer().subscribe(res => {
      this.valuesContainer = res;
    });
  }

  private _initLookups(){
    this.cityService.GetList().subscribe(res => {
      this.citiesList = res;
    });
  }

  private _initSocialVerificationMarks() {
    this.socialVerificationService.GetList(+this.selectedYear).subscribe(res => {
      this.socialVerificationsList = res;
    });

  }

  public getIndicatorValue(indicatorId: number, year: number) {
    let resList = this.valuesContainer.indicatorValuesList.filter(x => {
      return x.indicatorId == indicatorId && x.year == year && x.cityId == this.selectedCityId;
    });
    if (resList.length > 0)
      return resList[0].value;
    return '';
  }

  public getIndicatorIndexValue(indicatorIndexId: number, year: number) {
    let resList = this.valuesContainer.indicatorIndexesValuesList.filter(x => {
      return x.indicatorIndexId == indicatorIndexId && x.year == year && x.cityId == this.selectedCityId;
    });
    if (resList.length > 0)
      return resList[0].value;
    return '';
  }

  public applyFilters() {
    this.subIndexesList = this.subIndexesAllList.filter(x => this.filteredSubindexes.indexOf(x.subIndexId)>-1);
    this.showToast('Данные обновлены', 'info');
    console.log('Selected city: ' + this.selectedCityId);
  }

  editedIndicatorIndexId: number = -1;
  editedIndicatorIndexValue: number = -1;
  editIndexesChanges: IndicatorIndexValueModel[] = [];
  public setEditedIndicatorIndex(editedIndexValue: IndicatorIndexValueModel) {
    // If some index value was edited already - than add it to history to save
    if (this.editedIndicatorIndexId > 0) {
      let changeValRec = new IndicatorIndexValueModel(this.editedIndicatorIndexId, this.selectedCityId,
        this.editedIndicatorIndexValue, +this.selectedYear);
      this.indicatorIndexValueService.SaveIndicatorIndexValue(changeValRec).subscribe(res => {
        if (res) {
          this.showToast(`Значение показателя обновлено`, 'success');
          this._initValuesList();
          this.editedIndicatorIndexValue == -1;
        }
        else
          this.showToast('Что-то пошло не так', 'warn');
      });
      this.editIndexesChanges.push(changeValRec);

      // Update value in related indicator index
      editedIndexValue.value = this.editedIndicatorIndexValue;
    }

    this.editedIndicatorIndexId = editedIndexValue.indicatorIndexId;
    console.log('Change history is');
    console.log(this.editIndexesChanges);
  }

  public applyIndicatorChanges() {
    this.indicatorIndexValueService.SaveIndicatorIndexesChanges(this.editIndexesChanges).subscribe(res => {
      if (res) {
        this.showToast('Данные сохранены', 'success');
        this._initValuesList();
        this.editedIndicatorIndexValue == -1;
      }
      else
        this.showToast('Что-то пошло не так', 'warn');
    });
  }

  public isIndicatorHasRatingClass(indicator: IndicatorViewModel, checkedRatingClass) {
    let relatedIndicatorSocValues = this.socialVerificationsList.filter(x =>
      x.indicatorId == indicator.indicatorId && x.cityId == this.selectedCityId);
    let res = false;
    if (relatedIndicatorSocValues.length > 0)
      res = relatedIndicatorSocValues[0].ratingClass == checkedRatingClass;
    // console.log('Check verif: ' + checkedRatingClass);
    // console.log(indicator.indicatorId + ' ' + res);
    return res;
  }

  public getIndicatorSocialVerificationStr(indicatorId: number) {
    let relatedIndicatorSocValues = this.socialVerificationsList.filter(x =>
      x.indicatorId == indicatorId && x.cityId == this.selectedCityId);
    if (relatedIndicatorSocValues.length > 0) {
      return (this.socialVerificationsList.length - relatedIndicatorSocValues[0].ratingPosition + 1).toString()
        + '/' + this.socialVerificationsList.length;
    }
    return null;
  }
}
