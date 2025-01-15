import { Selector } from 'testcafe';
import Page from './pagesmodel/page';

const page = new Page();

fixture('Servidor')
  .page(page.urlBase)

test('Validando se esta de pe', async t => {
  await t.expect(page.tituloDaPagina.innerText).eql('Home Page - CursoOnline.Web')
})