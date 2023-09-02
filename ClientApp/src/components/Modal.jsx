
const Modal = () => {
    return (
        <div className={`w-full flex flex-col gap-4 h-full items-center justify-center absolute top-0 bg-slate-50`}>
            <form action="#" className="flex flex-col gap-2 bg-[#D9D9D9] items-center w-fit p-10 rounded-[20px] relative">
                <div className=" w-full bg-red-300 z-10">
                    <svg xmlns="http://www.w3.org/2000/svg" height="1em" viewBox="0 0 384 512" className="absolute right-0 mr-4 -mt-4">
                        <path d="M342.6 150.6c12.5-12.5 12.5-32.8 0-45.3s-32.8-12.5-45.3 0L192 210.7 86.6 105.4c-12.5-12.5-32.8-12.5-45.3 0s-12.5 32.8 0 45.3L146.7 256 41.4 361.4c-12.5 12.5-12.5 32.8 0 45.3s32.8 12.5 45.3 0L192 301.3 297.4 406.6c12.5 12.5 32.8 12.5 45.3 0s12.5-32.8 0-45.3L237.3 256 342.6 150.6z"/>
                    </svg>
                </div>
                <div className="w-full">
                    <label htmlFor="#" className="">Título:</label>
                    <input type="text" name="#" id="#" className="p-2 w-full rounded-xl border-black border-[1.8px]" />
                </div>
                <div className="w-full relative">
                    {/*<label htmlFor="#" className="absolute mt-2 pl-2 font-medium">Categoría</label>*/}
                    <input type="text" name="#" id="#" className="p-2 w-full rounded-xl border-black border-[1.8px] placeholder:text-black" placeholder="Categoría" />
                </div>
                <div className="w-full relative">
                    {/*<label htmlFor="#" className="absolute mt-2 pl-2 font-extralight">Descripción</label>*/}
                    <textarea rows={10} cols={55} className="rounded-xl border-black border-[1.5px] p-2 placeholder:text-black" placeholder="Descripción"/>
                </div>
                <div className="w-full">
                    <label htmlFor="#" className="absolute p-2 mt-1">
                        <svg xmlns="http://www.w3.org/2000/svg" height="1em" viewBox="0 0 384 512">
                            <path d="M215.7 499.2C267 435 384 279.4 384 192C384 86 298 0 192 0S0 86 0 192c0 87.4 117 243 168.3 307.2c12.3 15.3 35.1 15.3 47.4 0zM192 128a64 64 0 1 1 0 128 64 64 0 1 1 0-128z"/>
                        </svg>
                    </label>
                    <input type="text" name="#" id="#" className="p-2 w-full rounded-xl border-black border-[1.8px]" />
                </div>

                <div className="w-full">
                    <p className="">Subir Fotos y Videos</p>
                    <button className="border-black border-[1.8px] pl-4 pr-4 pt-2 pb-2 rounded-md">Examinar</button>
                </div>

                <div className="flex flex-col mt-4 w-full">
                    <div className="flex gap-4 justify-between text-start">
                        <p>¿Desea que su queja sea anónima?</p>
                        <input type="checkbox" name="" id="" />
                    </div>

                    <div className="flex gap-4 justify-between">
                        <p>Acepto los terminos y condiciones de convivencia</p>
                        <input type="checkbox" name="" id="" />
                    </div>
                </div>

                <button className="p-2 bg-[#1E3756] text-white w-full rounded-lg mt-4">Publicar</button>
            </form>
        </div>
    )
}

export default Modal
