import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

import { SubindexService, IndicatorValueService } from '../../@core/services'
import { SubindexViewModel, ValuesContainerModel } from "../../@core/model";
import { NbGlobalPhysicalPosition, NbToastrService } from "@nebular/theme";

@Component({
  selector: 'indicators-list',
  templateUrl: './indicators-list.component.html',
  styleUrls: ['./indicators-list.component.scss'],
  providers:[
    SubindexService, IndicatorValueService
  ]
})
export class IndicatorsListComponent implements OnInit {

  public subIndexesList: SubindexViewModel[] = [];
  private valuesContainer: ValuesContainerModel = this.indicatorValueService.GetEmptyContainer();

  constructor(
    private subindexService: SubindexService,
    private indicatorValueService: IndicatorValueService,
    private toastrService: NbToastrService,
    private router: Router){ }

  ngOnInit() {
    this._initSubindexesList();
    this._initValuesList();
  }

  private _initSubindexesList(){
    this.subindexService.GetVMList().subscribe(res => {
      this.subIndexesList = res;
    });
  }

  private _initValuesList(){
    this.indicatorValueService.GetValuesContainer().subscribe(res => {
      this.valuesContainer = res;
    });
  }

  public getIndicatorValue(indicatorId: number, year: number) {
    return this.valuesContainer.indicatorValuesList.filter(x => {
      return x.indicatorId == indicatorId && x.year == year;
    });
  }

  public getIndicatorIndexValue(indicatorIndexId: number, year: number) {
    return this.valuesContainer.indicatorIndexesValuesList.filter(x => {
      return x.indicatorIndexId == indicatorIndexId && x.year == year;
    });
  }
}
