export class FgvValidation {

    static getErrorMsg(
        fieldName: string,
        validatorName: string,
        validatorValue?: any
    ) {
        const config = {
            required: `${fieldName} é obrigatório.`,
            minlength: `${fieldName} precisa ter no mínimo ${validatorValue.requiredLength} caracteres.`,
            maxlength: `${fieldName} precisa ter no máximo ${validatorValue.requiredLength} caracteres.`,
            cepInvalido: 'CEP inválido.',
            email: 'Email inválido',
            equalsTo: 'Campos não são iguais',
            pattern: 'Campo inválido',
        };

        return config[validatorName];
    }
}
