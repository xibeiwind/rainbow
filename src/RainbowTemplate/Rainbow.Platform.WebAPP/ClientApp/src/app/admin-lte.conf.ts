export const adminLteConf = {
  skin: 'green',
  // isSidebarLeftCollapsed: false,
  // isSidebarLeftExpandOnOver: false,
  // isSidebarLeftMouseOver: false,
  // isSidebarLeftMini: true,
  // sidebarRightSkin: 'dark',
  // isSidebarRightCollapsed: true,
  // isSidebarRightOverContent: true,
  // layout: 'normal',
  sidebarLeftMenu: [
    { label: '主导航', separator: true },
    {
      label: '控制面板', route: 'dashboard', iconClasses: 'fa fa-tachometer',
      pullRights: [{ text: 'New', classes: 'label pull-right bg-green' }]
    },
    {
      label: '管理设置', iconClasses: 'fa fa-gears', children: [
        { label: '用户角色', route: 'admin/role' },
        // { label: 'FieldType', route: 'admin/data-field-type', iconClasses: 'fa fa-list' },
        // { label: 'Client Module', route: 'admin/client-module', iconClasses: 'fa fa-list' },
        // { label: 'Controller Project', route: 'admin/controller-project', iconClasses: 'fa fa-list' },
      ]
    },
    // { label: 'Model', route: 'model', iconClasses: 'fa fa-list' },
  ]
};
