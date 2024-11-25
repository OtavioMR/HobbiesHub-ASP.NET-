document.addEventListener('DOMContentLoaded', () => {
    // Função para carregar hobbies
    const loadHobbies = async () => {
        try {
            const response = await fetch('/api/hobbies'); // API que retorna os hobbies cadastrados
            const hobbies = await response.json();
            const hobbySelect = document.getElementById('hobby');
            
            hobbies.forEach(hobby => {
                const option = document.createElement('option');
                option.value = hobby.id; // ID do hobby
                option.textContent = hobby.nome; // Nome do hobby
                hobbySelect.appendChild(option);
            });
        } catch (error) {
            console.error('Erro ao carregar hobbies:', error);
        }
    };

    loadHobbies(); // Chama a função para carregar hobbies

    // Lógica para enviar os dados para a criação do grupo
    const submitButton = document.getElementById('submitButton');
    if (submitButton) {
        submitButton.addEventListener('click', async (e) => {
            e.preventDefault();
            const groupName = document.getElementById('group-name').value;
            const hobbyId = document.getElementById('hobby').value;

            if (groupName && hobbyId) {
                // Envia os dados para criar o grupo
                const groupData = {
                    nome: groupName,
                    hobbyId: hobbyId
                };

                const response = await fetch('/api/grupos', {
                    method: 'POST',
                    headers: {
                        'Content-Type': 'application/json'
                    },
                    body: JSON.stringify(groupData)
                });

                if (response.ok) {
                    alert(`Grupo '${groupName}' criado com sucesso!`);
                } else {
                    alert('Erro ao criar o grupo.');
                }
            } else {
                alert('Por favor, preencha todos os campos.');
            }
        });
    }
});
