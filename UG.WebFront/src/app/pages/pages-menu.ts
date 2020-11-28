import { NbMenuItem } from '@nebular/theme';

export const MENU_ITEMS: NbMenuItem[] = [
  {
    title: 'Перечень индикаторов',
    icon: 'list-outline',
    link: '/pages/indicators-list',
    home: true
  },
  {
    title: 'Источники метрик',
    icon: 'upload-outline',
    home: false,
    link: '/pages/outer-data-sources'
  }
];
