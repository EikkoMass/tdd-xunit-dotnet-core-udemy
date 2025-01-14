import { Selector } from 'testcafe';

fixture('Curso')
  .page('https://localhost:3000/Curso/Novo')

  test('Deve criar um novo curso', async t => {
      await t.typeText(Selector("[name='Nome']"), 'Curso TestCafe4')
      await t.typeText(Selector("[name='Descricao']"), 'teste')
      await t.typeText(Selector("[name='CargaHoraria']"), '10')
      await t.click(Selector("[name='PublicoAlvo']"))
      await t.click(Selector("option[value='Empregado']"))
      await t.click(Selector("[value='Online']"))
      await t.typeText(Selector("[name='Valor']"), '1000')

      await t.click(Selector(".btn-success"))
      
      await t.expect(Selector("title").innerText).eql('Listagem de cursos - CursoOnline.Web')
  })