document.addEventListener('DOMContentLoaded', () => {
    const submitButton = document.getElementById('submitButton');
    if (submitButton) {
        submitButton.addEventListener('click', (e) => {
            e.preventDefault();  // Impede o envio do formulário para testes
            const groupName = document.getElementById('groupName').value;
            if (groupName) {
                alert(`Grupo '${groupName}' criado com sucesso!`);
                // Aqui você pode adicionar a lógica de criação do grupo
            } else {
                alert('Por favor, insira o nome do grupo.');
            }
        });
    } else {
        console.error('Botão "Criar Grupo" não encontrado.');
    }
});
