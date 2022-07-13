import { Box, Button, CardMedia, Container, TextField } from '@mui/material';
import { useState } from 'react';
import { useEffect } from 'react';
import useProfileInfo from '../pages/Profile/hooks/useProfileInfo';
import validator from 'validator';

const MyProfile = () => {
  const [firstName, setFirstName] = useState('');
  const [lastName, setLastName] = useState('');
  const [email, setEmail] = useState('');
  const [bio, setBio] = useState('');
  const [profileImage, setProfileImage] = useState('');

  const { getInfo, updateMyInfo } = useProfileInfo();

  useEffect(() => {
    getInfo().then((data) => {
      console.log(data);
      setFirstName(data.firstname);
      setLastName(data.lastname);
      setEmail(data.email);
      if (data.image) {
        setProfileImage(data.image);
      }
      if (data.description) {
        setBio(data.description);
      }
    });
  }, []);

  const profileImageChangeHandler = (event) => {
    console.log(event.target.files[0]);
    let fileURL = URL.createObjectURL(event.target.files[0]);
    setProfileImage(fileURL);
  };

  const saveChangesHandler = () => {
    if (!firstName || !lastName || !email) {
      if (validator.isEmpty(firstName)) {
        alert('please enter first name');
      }
      if (validator.isEmpty(lastName)) {
        alert('please enter last name');
      }
      if (!validator.isEmail(email)) {
        alert('please enter email');
      }
      return;
    }
    const data = {
      id: '05fac5f9-0e84-408e-bf53-5c59fab87888',
      userName: 'nika21',
      firstname: firstName,
      lastname: lastName,
      email,
      bio,
      image: profileImage,
    };
    console.log(data);
    updateMyInfo(data).then((res) => console.log(res));
  };
  return (
    <Container
      sx={{
        display: 'flex',
        flexDirection: 'row',
        justifyContent: 'space-between',
        mb: 4,
        p: 2,
        border: '#000000',
      }}
    >
      <Box sx={{ display: 'flex', flexDirection: 'column' }}>
        <CardMedia
          component='img'
          height='300'
          width='300'
          image={profileImage}
          alt='Your Image'
          sx={{ mb: 1 }}
        />
        <Button variant='outlined' component='label'>
          Upload New image
          <input
            type='file'
            hidden
            accept='image/png, image/jpeg'
            onChange={profileImageChangeHandler}
          />
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
            onChange={(event) => setFirstName(event.target.value)}
            value={firstName}
            sx={{ mr: 1 }}
          />
          <TextField
            required
            label='Last Name'
            variant='outlined'
            value={lastName}
            onChange={(event) => setLastName(event.target.value)}
          />
        </Box>
        <TextField
          required
          fullWidth
          label='Email address'
          variant='outlined'
          type='email'
          onChange={(event) => setEmail(event.target.value)}
          value={email}
          sx={{ my: 1 }}
        />
        <TextField
          fullWidth
          label='Something About Yourself'
          variant='outlined'
          onChange={(event) => setBio(event.target.value)}
          value={bio}
          multiline
          minRows='2'
          sx={{ my: 1 }}
        />
        <br />
        <Button variant='contained' onClick={saveChangesHandler} sx={{ py: 1 }}>
          Save Changes
        </Button>
      </Box>
    </Container>
  );
};

export default MyProfile;
