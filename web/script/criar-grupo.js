document.getElementById("dropdownBtn").addEventListener("click", function() {
    document.getElementById("dropdownContent").classList.toggle("show");
});

// Captura o texto clicado pelo usuário e coloca no input
document.querySelectorAll("#dropdownContent a").forEach(function(item) {
    item.addEventListener("click", function(event) {
        event.preventDefault(); // Evita o comportamento padrão de links
        const selectedCategory = this.textContent; // Captura o texto da categoria clicada
        document.getElementById("category").value = selectedCategory; // Define no input
        document.getElementById("dropdownContent").classList.remove("show"); // Fecha o dropdown
    });
});

// Oculta o dropdown se o usuário clicar fora dele
window.onclick = function(event) {
    if (!event.target.matches('#dropdownBtn')) {
        var dropdowns = document.getElementsByClassName("dropdown-content");
        for (var i = 0; i < dropdowns.length; i++) {
            var openDropdown = dropdowns[i];
            if (openDropdown.classList.contains('show')) {
                openDropdown.classList.remove('show');
            }
        }
    }
};
