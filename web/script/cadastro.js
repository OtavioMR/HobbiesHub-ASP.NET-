document.addEventListener('DOMContentLoaded', function () {
    // Captura o formulário
    const form = document.querySelector('form');
    
    // Função de validação do formulário
    form.addEventListener('submit', function (event) {
        event.preventDefault(); // Previne o comportamento padrão do envio

        const nome = document.getElementById('nome').value.trim();
        const usuario = document.getElementById('usuario').value.trim();
        const email = document.getElementById('email').value.trim();
        const senha = document.getElementById('senha').value.trim();
        const confirmarSenha = document.getElementById('senha-confirmar').value.trim();

        // Validações simples
        if (!nome || !usuario || !email || !senha || !confirmarSenha) {
            alert('Todos os campos são obrigatórios!');
            return;
        }

        if (senha !== confirmarSenha) {
            alert('As senhas não coincidem!');
            return;
        }

        alert('Cadastro realizado com sucesso!');
        form.reset(); // Limpa o formulário após o cadastro
    });

    // Efeito hover para os ícones do footer
    const socialIcons = document.querySelectorAll('footer a');
    socialIcons.forEach(icon => {
        icon.addEventListener('mouseover', () => {
            icon.style.color = '#340a68'; // Cor de hover
        });
        icon.addEventListener('mouseout', () => {
            icon.style.color = ''; // Volta à cor original
        });
    });

    // Efeito de animação nos inputs do cadastro
    const inputFields = document.querySelectorAll('.cadastro-usuario');
    inputFields.forEach(input => {
        input.addEventListener('focus', () => {
            input.style.borderBottom = '4px solid #4f1fe2'; // Cor de foco
        });
        input.addEventListener('blur', () => {
            input.style.borderBottom = '4px solid black'; // Volta à cor original
        });
    });
});
