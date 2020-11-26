import { EmployeeManagementTemplatePage } from './app.po';

describe('EmployeeManagement App', function() {
  let page: EmployeeManagementTemplatePage;

  beforeEach(() => {
    page = new EmployeeManagementTemplatePage();
  });

  it('should display message saying app works', () => {
    page.navigateTo();
    expect(page.getParagraphText()).toEqual('app works!');
  });
});
