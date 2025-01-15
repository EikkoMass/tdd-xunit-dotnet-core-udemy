import { Selector } from 'testcafe';
import Curso from './pagesmodel/curso';

const curso = new Curso();

fixture('Curso')
  .page(`${curso.url}/Novo`)

  test('Deve criar um novo curso', async t => {
      await t.typeText(curso.inputNome, 'Curso TestCafe ' + (new Date()).toString());
      await t.typeText(curso.inputDescricao, 'teste')
      await t.typeText(curso.inputCargaHoraria, '10')
      await t.click(curso.inputPublicoAlvo)
      await t.click(curso.optionPublicoAlvo)
      await t.click(curso.inputRadio)
      await t.typeText(curso.inputValor, '1000')

      await t.click(Selector(".btn-success"))
      
      await t.expect(Selector("title").innerText).eql('Listagem de cursos - CursoOnline.Web')
  })

test('Deve evitar registrar curso com nome repetido', async t => {
    await t.typeText(Selector("[name='Nome']"), 'Curso TestCafe');
    await t.typeText(Selector("[name='Descricao']"), 'teste')
    await t.click(Selector(".btn-success"))

    await t.expect(Selector(".toast-message").withText('Nome do curso ja consta no banco de dados').count).eql(1);
})