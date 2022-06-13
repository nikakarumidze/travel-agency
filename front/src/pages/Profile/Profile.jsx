import {
  Box,
  Button,
  Card,
  CardContent,
  CardMedia,
  Container,
  TextField,
} from '@mui/material';
import React, { useState } from 'react';
import useForm from '../../hooks/useForm';

const Profile = () => {
  const { formState, onTypingHandler, validityChangeHandler } = useForm();
  const [description, setDescription] = useState();

  const saveChangesHandler = () => {
    if (!formState.firstName || !formState.lastName) {
      if (!formState.firstName.trim()) {
        validityChangeHandler('isFirstNameValid', false);
      }
      if (!formState.lastName.trim()) {
        validityChangeHandler('isLastNameValid', false);
      }
      // is email valid
      return;
    }
    console.log(description, formState);
  };
  return (
    <Container sx={{ m: 3, height: '370' }}>
      <Container
        sx={{
          display: 'flex',
          flexDirection: 'row',
          justifyContent: 'space-between',
          mb: 4,
          p: 2,
          border: '1px solid black',
        }}
      >
        <Box sx={{ display: 'flex', flexDirection: 'column' }}>
          <CardMedia
            component='img'
            height='300'
            width='300'
            image='https://picsum.photos/318/354'
            alt='Your Image'
            sx={{ mb: 1 }}
          />
          <Button variant='outlined' component='label'>
            Upload New image
            <input type='file' hidden accept='image/png, image/jpeg' />
          </Button>
        </Box>
        <Box
          sx={{
            display: 'flex',
            flexDirection: 'column',
            justifyContent: 'space-between',
          }}
        >
          <Box sx={{ display: 'flex', justifyContent: 'space-between', my: 1 }}>
            <TextField
              required
              label='First Name'
              variant='outlined'
              onChange={(event) =>
                onTypingHandler(event, 'firstName', 'isFirstNameValid')
              }
              sx={{ mr: 1 }}
              error={!formState.isFirstNameValid}
            />
            <TextField
              required
              label='Last Name'
              variant='outlined'
              onChange={(event) =>
                onTypingHandler(event, 'lastName', 'isLastNameValid')
              }
              error={!formState.isLastNameValid}
            />
          </Box>
          <TextField
            required
            fullWidth
            label='Email address'
            variant='outlined'
            type='email'
            onChange={(event) =>
              onTypingHandler(event, 'email', 'isEmailValid')
            }
            sx={{ my: 1 }}
          />
          <TextField
            fullWidth
            label='Something About Yourself'
            variant='outlined'
            onChange={(event) => setDescription(event.target.value)}
            multiline
            minRows='2'
            sx={{ my: 1 }}
          />
          <br />
          <Button
            variant='contained'
            onClick={saveChangesHandler}
            sx={{ py: 1 }}
          >
            Save Changes
          </Button>
        </Box>
      </Container>
      <Container>Dropdown</Container>
    </Container>
  );
};

export default Profile;
