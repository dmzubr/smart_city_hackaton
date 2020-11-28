import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

import { SubindexService, IndicatorValueService, CityService } from '../../@core/services'
import { SubindexViewModel, ValuesContainerModel, CityModel } from "../../@core/model";
import { NbGlobalPhysicalPosition, NbToastrService } from "@nebular/theme";

@Component({
  selector: 'indicators-list',
  templateUrl: './indicators-list.component.html',
  styleUrls: ['./indicators-list.component.scss'],
  providers:[
    SubindexService, IndicatorValueService, CityService
  ]
})
export class IndicatorsListComponent implements OnInit {

  public subIndexesAllList: SubindexViewModel[] = [];
  public subIndexesList: SubindexViewModel[] = [];
  private valuesContainer: ValuesContainerModel = this.indicatorValueService.GetEmptyContainer();

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
    private subindexService: SubindexService,
    private indicatorValueService: IndicatorValueService,
    private cityService: CityService,
    private toastrService: NbToastrService,
    private router: Router){ }

  ngOnInit() {
    this._initLookups();
    this._initSubindexesList();
    this._initValuesList();
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
      // this.filteredSubindexes = res.map(x => x.subIndexId);
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

  public getIndicatorValue(indicatorId: number, year: number) {
    return this.valuesContainer.indicatorValuesList.filter(x => {
      return x.indicatorId == indicatorId && x.year == year && x.cityId == this.selectedCityId;
    });
  }

  public getIndicatorIndexValue(indicatorIndexId: number, year: number) {
    return this.valuesContainer.indicatorIndexesValuesList.filter(x => {
      return x.indicatorIndexId == indicatorIndexId && x.year == year && x.cityId == this.selectedCityId;
    });
  }

  public applyFilters(s) {
    this.subIndexesList = this.subIndexesAllList.filter(x => this.filteredSubindexes.indexOf(x.subIndexId)>-1);
    this.showToast('Данные обновлены', 'info');
  }
}
