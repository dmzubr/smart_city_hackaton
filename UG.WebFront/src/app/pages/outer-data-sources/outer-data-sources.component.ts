import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

import { OuterDataSourceService } from '../../@core/services'
import { OuterDataSourceModelViewModel } from "../../@core/model";
import { NbGlobalPhysicalPosition, NbToastrService } from "@nebular/theme";

@Component({
  selector: 'outer-data-sources',
  templateUrl: './outer-data-sources.component.html',
  styleUrls: ['./outer-data-sources.component.scss'],
  providers:[
    OuterDataSourceService
  ]
})
export class OuterDataSourcesComponent implements OnInit {

  public dataSourcesList: OuterDataSourceModelViewModel[] = [];

  constructor(
    private metricDataSourceService: OuterDataSourceService,
    private toastrService: NbToastrService,
    private router: Router){ }

  ngOnInit() {
    this._initList();
  }

  private _initList(){
    this.metricDataSourceService.GetVMList().subscribe(res => {
      this.dataSourcesList = res;
    });
  }

  public openMetricValues(outerMetricId: number) {
    this.router.navigate([`pages/outer-metric-values/${outerMetricId}`]);
  }
}
