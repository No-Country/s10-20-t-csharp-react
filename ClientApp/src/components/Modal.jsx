import { useForm } from 'react-hook-form'
import axios from 'axios'
import { useState } from 'react'
import District from '../data/District'
import ImageUpload from './ImageUpload'

const Modal = () => {
    const { register, handleSubmit, formState: { errors } } = useForm()
    const [isOpen, setIsOpen] = useState(false)
    const [ubication, setUbication] = useState(false)
    const [close, setClose] = useState(false)

    const onSubmit = data => {
        data.district_ID = `${ubication}`
        data.photoAdress = localStorage.getItem('complaintImage')
        console.log(data)
        /*axios.post('https://s10nc.somee.com/api/Quejas', data).then(res => {
            console.log("Respuesta API: ", res)
        })*/
        console.log("id: ", ubication)
    }

    return (
        <div className={`w-full flex flex-col gap-4 h-full items-center justify-center absolute top-0 bg-slate-50 ${isOpen ? 'hidden' : 'flex'} `}>
            <form action="#" className="flex flex-col gap-2 bg-white border-[1px] border-black items-center w-fit p-10 rounded-[20px] relative" onSubmit={handleSubmit(onSubmit)}>
                <div className="w-full mt-5 bg-teal-500 relative cursor-pointer" onClick={() => setIsOpen(!isOpen)}>
                    <svg xmlns="http://www.w3.org/2000/svg" height="1em" viewBox="0 0 384 512" className="absolute z-10 right-0 mr-4 -mt-4">
                        <path d="M342.6 150.6c12.5-12.5 12.5-32.8 0-45.3s-32.8-12.5-45.3 0L192 210.7 86.6 105.4c-12.5-12.5-32.8-12.5-45.3 0s-12.5 32.8 0 45.3L146.7 256 41.4 361.4c-12.5 12.5-12.5 32.8 0 45.3s32.8 12.5 45.3 0L192 301.3 297.4 406.6c12.5 12.5 32.8 12.5 45.3 0s12.5-32.8 0-45.3L237.3 256 342.6 150.6z"/>
                    </svg>
                </div>
                <div className='flex justify-start items-center w-full'>
                    <details className="dropdown">
                        <summary className="m-1 btn capitalize bg-white text-black hover:bg-[#1E3756] hover:text-white" >Seleccione ubicación</summary>
                        <ul className={`p-2 shadow menu dropdown-content z-[1] bg-base-100 rounded-box w-52 ${close ? 'flex' : 'hidden'}`}>
                            {
                                District.map((item) => (
                                    <li key={item.District_ID} onClick={() => setClose(true)}><a className="text-xs" onClick={() => {
                                        setUbication(item.District_ID)
                                       // setClose(!close)
                                    }}>{item.Name}</a></li>
                                ))
                            }
                        </ul>
                    </details>
                </div>
                <div className="w-full">
                    <label htmlFor="#" className="text-black">Título:</label>
                    <input type="text" name="#" id="#" className="p-2 w-full rounded-xl border-black border-[1.8px] bg-white" {...register("title", { required: true })} />
                </div>
                <div className="w-full relative">
                    <input type="text" name="#" id="#" className="p-2 w-full rounded-xl bg-white border-black border-[1.8px] placeholder:text-black" placeholder="Categoría" {...register("category_ID", { required: true })} />
                </div>
                <div className="w-full relative">
                    <textarea rows={10} cols={55} className="rounded-xl bg-white border-black border-[1.5px] p-2 placeholder:text-black" placeholder="Descripción" {...register("text", { required: true })} />
                </div>

                <div className="w-full">
                    <p className="text-black mb-3">Subir Foto</p>
                    <ImageUpload />
                </div>

                <div className="flex flex-col mt-4 w-full">
                    <div className="flex gap-4 justify-between text-start text-black">
                        <p>¿Desea que su queja sea anónima?</p>
                        <input type="checkbox" name="" id="" className='bg-white' />
                    </div>

                    <div className="flex gap-4 justify-between text-black">
                        <p>Acepto los terminos y condiciones de convivencia</p>
                        <input type="checkbox" name="" id="" className='bg-white' />
                    </div>
                </div>

                <button className="p-2 bg-[#1E3756] text-white w-full rounded-lg mt-4">Publicar</button>
            </form>
        </div>
    )
}

export default Modal
