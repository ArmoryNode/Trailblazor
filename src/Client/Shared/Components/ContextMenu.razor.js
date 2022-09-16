export class ContextMenu {
    constructor(elementId) {
        this.elementRef = null;
        this.topOffset = 8;
        this.leftOffset = -8;
        this.elementRef = document.getElementById(elementId);
    }
    moveToTarget(targetElementId) {
        const targetRect = document.getElementById(targetElementId).getBoundingClientRect();
        const controlRect = this.elementRef.getBoundingClientRect();
        this.elementRef.style.left = ((targetRect.left - controlRect.width) + targetRect.width) + this.leftOffset + "px";
        this.elementRef.style.top = (targetRect.top + targetRect.height) + this.topOffset + "px";
    }
    moveToPointer(mouseEvent) {
        this.elementRef.style.left = mouseEvent.clientX + "px";
        this.elementRef.style.top = mouseEvent.clientY + "px";
    }
    overrideDefaultContextMenu(event) {
        event.preventDefault();
    }
    focusMenu() {
        this.elementRef.focus();
    }
}
export function create(elementId) {
    return new ContextMenu(elementId);
}
