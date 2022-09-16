export class ContextMenu {
    private elementRef: HTMLElement = null;
    private readonly topOffset: number = 8;
    private readonly leftOffset: number = -8;

    constructor(elementId: string) {
        this.elementRef = document.getElementById(elementId);
    }

    public moveToTarget(targetElementId: string): void {
        const targetRect = document.getElementById(targetElementId).getBoundingClientRect();
        const controlRect = this.elementRef.getBoundingClientRect();

        this.elementRef.style.left = ((targetRect.left - controlRect.width) + targetRect.width) + this.leftOffset + "px";
        this.elementRef.style.top = (targetRect.top + targetRect.height) + this.topOffset + "px";
    }

    public moveToPointer(mouseEvent: MouseEvent): void {
        //// If we want to override the default context menu and use this custom one in its place.
        //if (mouseEvent.button === mouseButtons.secondary && overrideDefaultContextMenu)
        //    mouseEvent.preventDefault();

        this.elementRef.style.left = mouseEvent.clientX + "px";
        this.elementRef.style.top = mouseEvent.clientY + "px";
    }

    public overrideDefaultContextMenu(event: MouseEvent) {
        event.preventDefault();
    }

    public focusMenu(): void {
        this.elementRef.focus();
    }
}

export function create(elementId: string): ContextMenu {
    return new ContextMenu(elementId);
}