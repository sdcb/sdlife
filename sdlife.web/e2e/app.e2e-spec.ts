import { LifePage } from './app.po';

describe('life App', () => {
  let page: LifePage;

  beforeEach(() => {
    page = new LifePage();
  });

  it('should display message saying app works', () => {
    page.navigateTo();
    expect(page.getParagraphText()).toEqual('app works!');
  });
});
