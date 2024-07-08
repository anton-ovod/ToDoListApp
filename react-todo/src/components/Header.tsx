import React from 'react'

const Header: React.FC<{headerTitle: string}> = (props) => {
  return (
    <header>
    <nav className="navbar justify-content-center navbar-light bg-light border-bottom box-shadow mb-3">
      <div className="d-flex justify-content-center">
        <a className="display-3 mx-0 text-decoration-none text-black">{props.headerTitle}</a>
      </div>
    </nav>
  </header>
  )
}

export default Header
