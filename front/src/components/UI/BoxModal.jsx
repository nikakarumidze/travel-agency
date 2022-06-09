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
          border: '1px solid rgba(0, 0, 0, 0.5)', p: 5
        }}
        noValidate
        autoComplete='off'
      >
        <Typography variant='h4' component='h1' align={props.alignTitle} gutterBottom>
          {props.title}
        </Typography>
        {props.children}
        <Button variant='outlined' onClick={props.onRegister} sx={{m:2}}>
          Register
        </Button>
        <Button variant='contained' onClick={props.onLogin} sx={{m:2}}>
          Login
        </Button>
      </Box>
    </Container>
  );
};

export default BoxModal;
