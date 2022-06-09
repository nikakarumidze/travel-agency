import React from 'react';
import Box from '@mui/material/Box';
import Button from '@mui/material/Button';
import Typography from '@mui/material/Typography';
import Container from '@mui/material/Container';

const BoxModal = (props) => {
  return (
    <Container align='center'>
      <Box
        component='form'
        sx={{
          border: '1px solid rgba(0, 0, 0, 0.5)', p: 5, maxWidth: 500
        }}
        noValidate
        autoComplete='off'
        onSubmit={props.onSubmit}
        
      >
        <Typography variant='h4' component='h1' align={props.alignTitle} gutterBottom>
          {props.title}
        </Typography>
        {props.children}
        <Box sx={{display: 'flex', justifyContent: 'space-between'}}>
        <Button variant={props.regVariant} onClick={props.onRegister} type={props.register} sx={{m:2}}>
          Register
        </Button>
        <Button variant={props.logVariant} onClick={props.onLogin} type={props.login} sx={{m:2}}>
          Login
        </Button>
        </Box>
      </Box>
    </Container>
  );
};

export default BoxModal;
