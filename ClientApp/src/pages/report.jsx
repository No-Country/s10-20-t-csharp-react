import { useState } from "react"

const Report = () => {
    const [isOpen, setIsOpen] = useState(false)

    return (
        <>
            <div className="flex flex-col w-full items-start justify-center">
                <div className="flex gap-4 items-center w-full p-10">
                    <div className="">
                        <h2 className="text-xl font-medium">La convivencia ciudadana y cuidar los espacios comunes es una responsabilidad colectiva.</h2>

                        <p className="font-light text-lg mt-12">El estado es el encargado de mantener estos espacios en optimas condiciones de  funcionamiento, seguridad e higiene. </p>
                        <h2 className="text-lg font-medium mt-4">¿Qué pasa cuando esto no funciona como debería?</h2>
                        <p className="font-light mt-3">Tu reporte como ciudadano colabora con la visibilización de estas situaciones que necesitan ser atendidas con urgencia. </p>
                    </div>
                    <div className="w-[50%] flex flex-col gap-4 items-center mt-12" >
                        <button className="bg-[#D9D9D9] w-full p-5 rounded-xl font-medium" onClick={() => setIsOpen(!isOpen)}>Nuevo Reporte</button>
                        <button className="bg-[#D9D9D9] w-full pl-7 pr-7 pt-5 pb-5 rounded-xl font-medium ">Mis Reportes</button>
                    </div>
                </div>
                <div className={`w-full  ${isOpen ? "hidden" : "flex" } flex-col gap-4 h-full items-center justify-center`}>
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

                <div className="m-8 w-full">
                    <p className="text-start font-medium">Preguntas Frecuentes</p>

                    <div className="w-[50%] h-20 bg-[#D9D9D9]">

                    </div>
                </div>
            </div>
        </>
    )
}

export default Report
