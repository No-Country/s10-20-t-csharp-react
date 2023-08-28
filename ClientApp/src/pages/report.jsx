const Report = () => {
    return (
        <>
            <div className="flex w-full items-start justify-between mt-10">
                <div className="w-[50%] flex flex-col gap-4 items-center">
                    <button className="bg-[#D9D9D9] w-fit p-5 rounded-xl">Nuevo Reporte</button>
                    <button className="bg-[#D9D9D9] w-fit pl-7 pr-7 pt-5 pb-5 rounded-xl">Mis Reportes</button>
                </div>

                <div className="w-[100%] flex flex-col gap-4 h-full items-center">
                    <form action="#" className="flex flex-col gap-2 bg-[#D9D9D9] items-center w-fit p-6 rounded-[20px]">
                        <div className="w-full relative">
                            <label htmlFor="#" className="absolute mt-2 pl-2">Título:</label>
                            <input type="text" name="#" id="#" className="p-2 w-full" />
                        </div>
                        <div className="w-full relative">
                            <label htmlFor="#" className="absolute mt-2 pl-2">Categoría:</label>
                            <input type="text" name="#" id="#" className="p-2 w-full" />
                        </div>
                        <div className="w-full relative">
                            <label htmlFor="#" className="absolute mt-2 pl-2">Descripción</label>
                            <textarea rows={10} cols={55} />
                        </div>
                        <div className="w-full">
                            <label htmlFor="#" className="ml-3">Etiquetar Ubicación</label>
                            <input type="text" name="#" id="#" className="p-2 w-full" />
                        </div>

                        <div className="w-full">
                            <p className="ml-3">Subir Fotos</p>
                            <button className="bg-white pl-4 pr-4 pt-2 pb-2 rounded-md">Examinar</button>
                        </div>

                        <div className="flex flex-col mt-4">
                            <div className="flex gap-4 justify-between">
                                <p>¿Desea que su queja sea anónima?</p>
                                <input type="checkbox" name="" id="" />
                            </div>

                            <div className="flex gap-4 justify-between">
                                <p>Acepto los terminos y condiciones de convivencia</p>
                                <input type="checkbox" name="" id="" />
                            </div>
                        </div>

                        <button className="p-2 bg-white w-fit rounded-lg">Publicar</button>
                    </form>
                </div>
            </div>
        </>
    )
}

export default Report
