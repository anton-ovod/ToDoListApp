import React from 'react';
import { ClipLoader } from 'react-spinners';

const override = {
    display: 'block',
    margin: '100px auto',
}
const Spinner: React.FC<{loading: boolean}> = (props) => {
  return (
    <ClipLoader 
    color='#4338ca'
    loading={ props.loading }
    cssOverride={ override }
    size={150}
    />
  )
}

export default Spinner