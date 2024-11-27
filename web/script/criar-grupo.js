document.addEventListener("DOMContentLoaded", () => {
    const hobbySelect = document.getElementById("hobby");
    const userId = "USER_LOGGED_ID"; // Substitua com a lógica para pegar o ID do usuário logado

    // Função para carregar hobbies do backend
    async function loadHobbies() {
        try {
            const response = await fetch("URL_DA_API_HOBBIES");
            const hobbies = await response.json();

            hobbies.forEach(hobby => {
                const option = document.createElement("option");
                option.value = hobby.id; // ID do hobby
                option.textContent = hobby.nome; // Nome do hobby
                hobbySelect.appendChild(option);
            });
        } catch (error) {
            console.error("Erro ao carregar hobbies:", error);
        }
    }

    // Capturar dados do formulário ao submeter
    document.getElementById("group-form").addEventListener("submit", async (e) => {
        e.preventDefault();

        const groupName = document.getElementById("group-name").value;
        const hobbyId = hobbySelect.value;
        const maxPeople = document.getElementById("max-people").value;

        if (!hobbyId) {
            alert("Por favor, selecione um hobby!");
            return;
        }

        const groupData = {
            NomeGrupo: groupName,
            HobbyId: hobbyId,
            AdministradorId: userId, // ID do usuário logado
            MaxPessoas: maxPeople,
        };

        try {
            const response = await fetch("URL_DA_API_GRUPOS", {
                method: "POST",
                headers: {
                    "Content-Type": "application/json"
                },
                body: JSON.stringify(groupData)
            });

            if (response.ok) {
                alert("Grupo criado com sucesso!");
            } else {
                alert("Erro ao criar grupo.");
            }
        } catch (error) {
            console.error("Erro ao enviar dados do grupo:", error);
        }
    });

    // Chamar a função para carregar hobbies
    loadHobbies();
});
