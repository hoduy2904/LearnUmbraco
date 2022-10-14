const load=(img)=>{
    const url=img.getAttribute('lazy-src')

    img.setAttribute('src',url);

}


const ready=()=>{
    if('IntersectionObserver' in window){
        var lazyImg=document.querySelectorAll('[lazy-src]');

        let Observer=new IntersectionObserver((entries)=>{
            entries.forEach(entry=>{
                if(entry.isIntersecting){
                    load(entry.target)
                }
            })
        })
        
        lazyImg.forEach(img=>{
            Observer.observe(img)
        })
    }else{

    }
}


document.addEventListener('DOMContentLoaded', ready)