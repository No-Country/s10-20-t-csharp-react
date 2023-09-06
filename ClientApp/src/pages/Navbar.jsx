import React from 'react'

const Navbar = () => {
  return (
    <div>
    <div className="navbar bg-base-100">
          <div className="navbar-start">
              <div className="dropdown">
                      <label tabIndex={0} className="">
                        <a className='whitespace-nowrap text-yellow-500 text-xl ml-6 font-bold'>Red Co.</a>
                      </label>
                      <ul tabIndex={0} className="menu menu-sm dropdown-content mt-3 z-[1] p-2 shadow bg-base-100 rounded-box w-52">
                          <li><a>Muro</a></li>
                          <li><a>Nuevo Reporte</a></li>
                          <li><a>Log Out</a></li>
                      </ul>
              </div>
          </div>
    <div className="navbar-end mr-28">
                  <a className="text-sm cursor-pointer">Muro</a>
                  <a className="text-sm ml-2 whitespace-nowrap cursor-pointer">Nuevo Reporte</a>
              <button className="btn btn-ghost btn-circle">
                 <div className="indicator">
                 <div className="dropdown">
                      <label tabIndex={0} className="">
                        <svg xmlns="http://www.w3.org/2000/svg" className="h-5 w-5" fill="none" viewBox="0 0 24 24" stroke="currentColor"><path strokeLinecap="round" strokeLinejoin="round" strokeWidth="2" d="M15 17h5l-1.405-1.405A2.032 2.032 0 0118 14.158V11a6.002 6.002 0 00-4-5.659V5a2 2 0 10-4 0v.341C7.67 6.165 6 8.388 6 11v3.159c0 .538-.214 1.055-.595 1.436L4 17h5m6 0v1a3 3 0 11-6 0v-1m6 0H9" /></svg>
                      </label>
                        <ul tabIndex={0} className="dropdown-content z-[1] menu p-2 shadow bg-base-100 rounded-box w-52 mr-12">
                          <p>Notificaciones</p>
                            <li className='mt-6'>
                              <div>
                                <p className='text-xs text-gray-300'><b className='text-black font-bold'>Lorena Casas</b> ha comentado tu foto</p>
                              </div>
                            </li>
                            <li className='mt-2'>
                              <div>
                                <p className='text-xs text-gray-300'><b className='text-black font-bold'>Lorena Casas</b> ha comentado tu foto</p>
                              </div>
                            </li>
                        </ul>
                    </div>
                    
                      
                </div>
             </button>
              <button className="btn btn-ghost btn-circle">
                  <svg xmlns="http://www.w3.org/2000/svg" className="h-5 w-5" fill="none" viewBox="0 0 24 24" stroke="currentColor"><path strokeLinecap="round" strokeLinejoin="round" strokeWidth="2" d="M21 21l-6-6m2-5a7 7 0 11-14 0 7 7 0 0114 0z" /></svg>
              </button>
   </div>
 </div>
</div>
  )
}

export default Navbar

/*import React from 'react'

const Navbar = () => {
  return (
    <div>
      <div className="navbar bg-base-100">
            <div className="navbar-start">
                <div className="dropdown">
                        <label tabIndex={0} className="btn btn-ghost btn-circle">
                            <svg xmlns="http://www.w3.org/2000/svg" className="h-5 w-5" fill="none" viewBox="0 0 24 24" stroke="currentColor"><path strokeLinecap="round" strokeLinejoin="round" strokeWidth="2" d="M4 6h16M4 12h16M4 18h7" /></svg>
                          
                        
                        </label>
                        <ul tabIndex={0} className="menu menu-sm dropdown-content mt-3 z-[1] p-2 shadow bg-base-100 rounded-box w-52">
                            <li><a>Homepage</a></li>
                            <li><a>Portfolio</a></li>
                            <li><a>About</a></li>
                        </ul>
                </div>
            </div>
      <div className="navbar-end">
                    <a className="text-sm cursor-pointer">Muro</a>
                    <a className="text-sm ml-2 whitespace-nowrap cursor-pointer">Nuevo Reporte</a>
                <button className="btn btn-ghost btn-circle">
                   <div className="indicator">
                        <svg xmlns="http://www.w3.org/2000/svg" className="h-5 w-5" fill="none" viewBox="0 0 24 24" stroke="currentColor"><path strokeLinecap="round" strokeLinejoin="round" strokeWidth="2" d="M15 17h5l-1.405-1.405A2.032 2.032 0 0118 14.158V11a6.002 6.002 0 00-4-5.659V5a2 2 0 10-4 0v.341C7.67 6.165 6 8.388 6 11v3.159c0 .538-.214 1.055-.595 1.436L4 17h5m6 0v1a3 3 0 11-6 0v-1m6 0H9" /></svg>
                        <span className="badge badge-xs badge-primary indicator-item"></span>
                  </div>
               </button>
                <button className="btn btn-ghost btn-circle">
                    <svg xmlns="http://www.w3.org/2000/svg" className="h-5 w-5" fill="none" viewBox="0 0 24 24" stroke="currentColor"><path strokeLinecap="round" strokeLinejoin="round" strokeWidth="2" d="M21 21l-6-6m2-5a7 7 0 11-14 0 7 7 0 0114 0z" /></svg>
                </button>
     </div>
   </div>
  </div>
  )
}

export default Navbar
*/