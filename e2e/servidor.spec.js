import { Selector } from 'testcafe';

fixture('Servidor')
  .page('https://localhost:3000')

test('Validando se esta de pe', async t => {
  await t.expect(Selector('title').innerText).eql('Home Page - CursoOnline.Web')
})