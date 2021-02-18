export class ColorInfo{
    
    constructor(public baseColor: string, public gradient: number, public hoverGradient: number) {                
    }

    getBackground(){
        return `bg-${this.baseColor}-${this.gradient}`;
    }

    getHover(){
        return `hover:bg-${this.baseColor}-${this.hoverGradient}`;
    }
}