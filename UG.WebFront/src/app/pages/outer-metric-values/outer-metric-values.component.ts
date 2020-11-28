import { Component, OnInit } from '@angular/core';
import {ActivatedRoute, Router} from '@angular/router';

import { OuterMetricCityValueService } from '../../@core/services'
import { OuterMetricValueViewModel } from "../../@core/model";
import { NbGlobalPhysicalPosition, NbToastrService } from "@nebular/theme";

@Component({
  selector: 'outer-metric-values',
  templateUrl: './outer-metric-values.component.html',
  styleUrls: ['./outer-metric-values.component.scss'],
  providers:[
    OuterMetricCityValueService
  ]
})
export class OuterMetricValuesComponent implements OnInit {

  private sub: any;
  public metricId: number = 0;
  public metricValues: OuterMetricValueViewModel[] = [];

  constructor(
    private route: ActivatedRoute,
    private metricValuesService: OuterMetricCityValueService,
    private toastrService: NbToastrService){ }

  ngOnInit() {
    this.sub = this.route.params.subscribe(params => {
      this.metricId = +params['metric-id'];
      if (this.metricId > 0) {
        this._initList();
      }
    });
  }

  private _initList(){
    this.metricValuesService.GetValuesByMetric(this.metricId).subscribe(res => {
      this.metricValues = res;
    });
  }
}
