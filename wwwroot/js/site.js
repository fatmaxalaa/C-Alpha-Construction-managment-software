function dropdown() {
    document.getElementById("sub-menu").classList.toggle("reveal")
}
document.getElementById("arrow").addEventListener("click", dropdown);



//DragAndDrop

//selectors
const draggable = document.querySelectorAll(".draggable");
const containers = document.querySelectorAll(".containerr");



draggable.forEach(draggable => {
    draggable.addEventListener("dragstart", () => {
        draggable.classList.add("dragging");
    })
    draggable.addEventListener("dragend", () => {
        draggable.classList.remove("dragging");
    })
})


containers.forEach(container => {
    container.addEventListener("dragover", e => {
        e.preventDefault();
        const afetrElement = getDragAfterElement(container, e.clientY);
        const draggable = document.querySelector(".dragging");
        if (afetrElement == null) {
            container.appendChild(draggable);
        }
        else {
            container.insertBefore(draggable, afetrElement)
        }

    })
})


function getDragAfterElement(container, y) {
    const draggableElements = [...container.querySelectorAll(".draggable:not(.dragging)")];

    return draggableElements.reduce((closest, child) => {
        const box = child.getBoundingClientRect();
        const offset = y - box.top - box.height / 2;
        console.log(offset);

        if (offset < 0 && offset > closest.offset) {
            return { offset: offset, element: child }
        }
        else {
            return closest;
        }
    }, { offset: Number.NEGATIVE_INFINITY }).element
}