document.getElementById("dropdownBtn").addEventListener("click", function() {
    const content = document.getElementById("dropdownContent");
    if (content.style.display === "block") {
        content.style.display = "none";
        this.textContent = "Escolha uma categoria ▼"; // Muda o texto do botão
    } else {
        content.style.display = "block";
        this.textContent = "Escolha uma categoria ▲"; // Muda o texto do botão
    }
});
