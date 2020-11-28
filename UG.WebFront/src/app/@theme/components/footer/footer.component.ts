import { Component } from '@angular/core';

@Component({
  selector: 'ngx-footer',
  styleUrls: ['./footer.component.scss'],
  template: `
    <span class="created-by"><a href="https://cashee.ru" target="_blank">Ботаники</a>&nbsp;2020</span>
    <div class="socials">
      <a href="https://github.com/dmzubr" target="_blank" class="ion ion-social-github"></a>
    </div>
  `,
})
export class FooterComponent {
}
