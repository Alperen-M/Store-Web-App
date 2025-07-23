window.onload = function () {
    const infoBox = document.createElement("div");
    infoBox.innerHTML = "<div style='padding: 10px; background: #fffbdd; border-left: 5px solid #ffd700; margin-bottom: 20px;'><strong>İpucu:</strong> Ürünleri listelemek için <code>/api/store/products</code> endpoint'ini kullanabilirsin.</div>";
    document.querySelector(".swagger-ui").prepend(infoBox);
};
