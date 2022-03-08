import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class SlideShowServiceService {
  constructor() { }
  public getImagens(): any[]{
    return GALERIAS;
  }
}

export const GALERIAS = [
  {src: '../../../../assets/carousel/home/img1.jpg',  title: ''  },
  {src: '../../../../assets/carousel/home/img2.jpg',  title: ''  },
  {src: '../../../../assets/carousel/home/img3.jpg',  title: ''  },
  {src: '../../../../assets/carousel/home/img4.jpg',  title: ''  },
  {src: '../../../../assets/carousel/home/img5.jpg',  title: ''  },
  {src: '../../../../assets/carousel/home/img6.jpg',  title: ''  },
  {src: '../../../../assets/carousel/home/img7.jpg',  title: ''  },
  {src: '../../../../assets/carousel/home/img8.jpg',  title: ''  },
  {src: '../../../../assets/carousel/home/img9.jpg',  title: ''  },
  {src: '../../../../assets/carousel/home/img10.jpg',  title: ''  },
  {src: '../../../../assets/carousel/home/img11.jpg',  title: ''  },
  {src: '../../../../assets/carousel/home/img12.jpg',  title: ''  }
] 