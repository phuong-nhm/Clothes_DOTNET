
const listImage = document.querySelector('.list-images')
const imgs = document.querySelectorAll('.img-banner')
const btnLeft = document.querySelector('.btn-left')
const btnRight = document.querySelector('.btn-right')
length = imgs.length
let current = 0 
const handleChangeSlide = () => {
    let width = imgs[0].offsetWidth
    if(current== length - 1){
        current = 0
        
        listImage.style.transform = `translateX(0px)`
        document.querySelector('.active').classList.remove('active')
        document.querySelector('.index-item-'+current).classList.add("active")
    }else{
        current ++
        listImage.style.transform = `translateX(${width * -1 * current}px)`
        document.querySelector('.active').classList.remove('active')
        document.querySelector('.index-item-'+current).classList.add("active")
    }
}
let handleEventChangeSlide = setInterval(handleChangeSlide,4000)
btnRight.addEventListener('click',()=>{
    clearInterval(handleEventChangeSlide)
    handleChangeSlide()
    document.querySelector('.active').classList.remove('active')
    document.querySelector('.index-item-'+current).classList.add("active")
    handleEventChangeSlide = setInterval(handleChangeSlide,4000)
})
btnLeft.addEventListener('click',()=>{
    clearInterval(handleEventChangeSlide)
    if(current== 0){
        current = length - 1
        let width = imgs[0].offsetWidth
        listImage.style.transform = `translateX(${width * -1 * current}px)`
        
    }else{
        current --
        let width = imgs[0].offsetWidth
        listImage.style.transform = `translateX(${width * -1 * current}px)`
    }
    document.querySelector('.active').classList.remove('active')
    document.querySelector('.index-item-'+current).classList.add("active")
    handleEventChangeSlide = setInterval(handleChangeSlide,4000)
})
