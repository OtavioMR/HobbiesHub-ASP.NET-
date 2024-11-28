// Função para carregar hobbies na lista
async function loadHobbies() {
    try {
        const response = await fetch('https://localhost:44327/api/Hobby');
        const hobbies = await response.json();

        const categorySelect = document.getElementById('category'); // ID do campo de seleção de hobby
        if (!categorySelect) {
            console.error('Elemento "category" não encontrado');
            return;
        }
        
        categorySelect.innerHTML = ''; // Limpar as opções antigas

        hobbies.forEach(hobby => {
            const option = document.createElement('option');
            option.value = hobby.id; // Definir o ID do hobby como o valor
            option.textContent = hobby.nameHobby; // Exibir o nome do hobby
            categorySelect.appendChild(option);
        });
    } catch (error) {
        console.error('Erro ao carregar hobbies:', error);
    }
}

// Função para obter o ID do usuário logado
function getLoggedUserId() {
    // Simulando um ID de usuário logado. Aqui você pode adicionar a lógica para obter o ID real do usuário logado.
    return 'usuario_logado_id'; // Substitua isso com a lógica correta.
}

// Função para criar o grupo
async function createGroup(event) {
    event.preventDefault(); // Impede o envio padrão do formulário

    const groupName = document.getElementById('group-name').value; // Nome do grupo
    const groupDescription = document.getElementById('group-description').value; // Descrição do grupo
    const categoryId = document.getElementById('category').value; // ID do hobby selecionado
    const adminId = getLoggedUserId(); // ID do administrador (usuário logado)

    // Validação do formulário
    if (!groupName || !groupDescription || !categoryId) {
        alert('Por favor, preencha todos os campos!');
        return;
    }

    const groupData = {
        nomeGrupo: groupName,
        descricaoGrupo: groupDescription,
        hobbyId: categoryId, // Usando o ID do hobby
        administradorId: adminId,
    };

    try {
        const response = await fetch('https://localhost:44327/api/Grupo', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(groupData),
        });

        if (!response.ok) {
            const errorData = await response.json();
            alert(`Erro ao criar grupo: ${errorData.message}`);
            return;
        }

        const createdGroup = await response.json();
        alert(`Grupo criado com sucesso! ID do grupo: ${createdGroup.id}`);
        // Redirecionar ou limpar o formulário
        console.log('Redirecionando para a página de grupos...');
        window.location.href = './mostrar-grupos.html'; // Caminho absoluto
    } catch (error) {
        console.error('Erro ao criar grupo:', error);
        alert('Erro ao criar grupo. Tente novamente mais tarde.');
    }
}

// Chama a função para carregar os hobbies ao carregar a página
document.addEventListener('DOMContentLoaded', function() {
    loadHobbies(); // Carregar os hobbies
    document.getElementById('group-form').addEventListener('submit', createGroup); // Adicionar evento de envio ao formulário
});
