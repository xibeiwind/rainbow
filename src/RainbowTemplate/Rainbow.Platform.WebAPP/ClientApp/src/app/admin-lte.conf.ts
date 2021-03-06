export const adminLteConf = {
    skin: 'yellow-light',
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
        { label: '控制面板', route: 'dashboard', iconClasses: 'fa fa-tachometer', pullRights: [{ text: 'New', classes: 'label pull-right bg-green' }] },
        { label: '登录', iconClasses: "fa fa-login", route: 'auth' }
    ]
};
