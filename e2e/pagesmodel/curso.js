import { Selector } from 'testcafe';
import Page from './page';

export default class Curso extends Page {
    constructor() {
        super();
        this.url = `${this.urlBase}/Curso`;
        this.inputNome = Selector("[name='Nome']");
        this.inputDescricao = Selector("[name='Descricao']");
        this.inputCargaHoraria = Selector("[name='CargaHoraria']");
        this.inputPublicoAlvo = Selector("[name='PublicoAlvo']");
        this.optionPublicoAlvo = Selector("option[value='Empregado']");
        this.inputRadio = Selector("[value='Online']");
        this.inputValor = Selector("[name='Valor']");
    }
}